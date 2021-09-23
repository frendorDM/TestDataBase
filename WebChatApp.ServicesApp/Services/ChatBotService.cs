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

            // Call the search.list method to retrieve results matching the specified query term.
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

                        //case "youtube#channel":
                        //    channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        //    break;
                }
            }
            return "LOX";
            
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

            // Call the search.list method to retrieve results matching the specified query term.
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
                messageInputDto.UserCreatorId = 5;
                await _messageService.AddMessage(messageInputDto);
            }
            else if (messageForBot.Contains("//info"))
            {

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
