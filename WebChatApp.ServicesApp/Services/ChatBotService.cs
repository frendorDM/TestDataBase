using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using RestSharp;
using WebChatApp.Core;
using WebChatApp.Core.Enum;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;

namespace WebChatApp.ServicesApp
{
    public class ChatBotService : IChatBotService
    {
        private readonly ISession _session;
        private IMapper _mapper;
        private IMessageService _messageService;
        public ChatBotService(ISession session, IMapper mapper, IMessageService messageService)
        {
            _session = session;
            _mapper = mapper;
            _messageService = messageService;
        }
        public async Task<string> GetVideoId(string videoName)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBsh8XU8bAL1FtP5GECDDnscMUTmY8o41A",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");

            searchListRequest.Q = videoName; // Replace with your search term.
            searchListRequest.MaxResults = 1;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var a = youtubeService.CommentThreads.List("snippet");
            a.VideoId = searchListResponse.Items[0].Id.VideoId;
            //a.Id = "E9AJvGkfAiU";
            //a.MaxResults = 1;
            var aresp = await a.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {

                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        string v = searchResult.Id.VideoId;
                        return v;
                }
            }
            return "We couldn't find a video with that name";
            
        }
        public async Task<string> GetChannelId(string channelName)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBsh8XU8bAL1FtP5GECDDnscMUTmY8o41A",
                ApplicationName = this.GetType().ToString()
            });

            //var searchListRequest = youtubeService.Search.List("snippet");
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = channelName; // Replace with your search term.
            searchListRequest.MaxResults = 5;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> channels = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {

                switch (searchResult.Id.Kind)
                {
                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        string v = searchResult.Snippet.Title;
                        return v;
                }
            }
            return "We couldn't find a video with that name";

        }
        public async Task<string> GetCommentFromVideo()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBsh8XU8bAL1FtP5GECDDnscMUTmY8o41A",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");

            searchListRequest.Q = "Лекция 1. Генезис операционных систем. Назначение ОС. Базовые принципы организации ОС"; // Replace with your search term.
            searchListRequest.MaxResults = 1;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var a = youtubeService.CommentThreads.List("snippet");
            a.VideoId = searchListResponse.Items[0].Id.VideoId;
            //a.Id = "E9AJvGkfAiU";
            //a.MaxResults = 1;
            var aresp = await a.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                        //case "youtube#channel":
                        //    channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        //    break;
                }
            }

            string d = String.Format(aresp.Items[0].Snippet.TopLevelComment.Snippet.TextOriginal);
            return d;

        }
        public async Task<List<string>> GetViewCountFromVideo(string videoId)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBsh8XU8bAL1FtP5GECDDnscMUTmY8o41A",
                ApplicationName = this.GetType().ToString()
            });

            var views = youtubeService.Videos.List("statistics");
            //views.Part = statistics;
            views.Id = videoId;
            var aresp = await views.ExecuteAsync();
            List<string> viewCountList = new List<string>();
            var viewCount = aresp.Items[0].Statistics.ViewCount;
            viewCountList.Add(viewCount.ToString());
            var likeCount = aresp.Items[0].Statistics.LikeCount;
            viewCountList.Add(likeCount.ToString());
            return viewCountList;

        }

        public async Task AddMessageBot(MessageInputDto messageDto)
        {
            var messageModel = _mapper.Map<MessageEntity>(messageDto);
            await _session.AddEntityAsync(messageModel);
            var messageForBot = messageDto.Text;
            if (messageForBot.Contains("//find"))
            {
                var strings = messageForBot.Split("||");
                string videoId = await GetVideoId(strings[1]);
                string linkToVideo = "https://www.youtube.com/watch?v=" + videoId;
                MessageInputDto messageInputDto = new MessageInputDto();
                messageInputDto.ChatId = messageDto.ChatId;
                messageInputDto.Text = linkToVideo;
                messageInputDto.isDeleted = false;
                messageInputDto.UserCreatorId = (int)UserType.Bot;
                if (messageForBot.Contains("-v-"))
                {
                    var viewCountList = await GetViewCountFromVideo(videoId);
                    messageInputDto.Text = $"{linkToVideo} просмотры: {viewCountList[0] }";
                    if (messageForBot.Contains("-v-|-"))
                    {
                        messageInputDto.Text = $"{linkToVideo} просмотры: {viewCountList[0]}, лайки:{viewCountList[1]} ";
                    }
                }
                
                await _messageService.AddMessage(messageInputDto);
            }
            else if (messageForBot.Contains("//info"))
            {
                string channelNameFromUser = messageForBot.Remove(0, 6);
                string channelName = await GetChannelId(channelNameFromUser);
                MessageInputDto messageInputDto = new MessageInputDto();
                messageInputDto.ChatId = messageDto.ChatId;
                messageInputDto.Text = channelName;
                messageInputDto.isDeleted = false;
                messageInputDto.UserCreatorId = (int)UserType.Bot;
                await _messageService.AddMessage(messageInputDto);

            }
            else if (messageForBot.Contains("//videoCommentRandom"))
            {

            }
            else if (messageForBot.Contains("//help"))
            {
            
            }
        }
    }
}
