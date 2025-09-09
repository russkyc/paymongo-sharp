// MIT License
// 
// Copyright (c) 2023 Russell Camo (@russkyc)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Net.Http;
using System.Threading.Tasks;
using Paymongo.Sharp.Features.Links.Contracts;
using Paymongo.Sharp.Helpers;

namespace Paymongo.Sharp.Features.Links
{
    public class LinksClient
    {
        private const string Resource = "/links";
        private readonly HttpClient _client;

        public LinksClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Link> CreateLinkAsync(Link link)
        {
            return await _client.SendRequestAsync<Link>(HttpMethod.Post, Resource, link, content => content.ToLink());
        }

        public async Task<Link> RetrieveLinkAsync(string id)
        {
            return await _client.SendRequestAsync<Link>(HttpMethod.Get, $"{Resource}/{id}", responseDeserializer: content => content.ToLink());
        }

        public async Task<Link> ArchiveLinkAsync(string id)
        {
            return await _client.SendRequestAsync<Link>(HttpMethod.Post, $"{Resource}/{id}/archive", responseDeserializer: content => content.ToLink());
        }

        public async Task<Link> UnarchiveLinkAsync(string id)
        {
            return await _client.SendRequestAsync<Link>(HttpMethod.Post, $"{Resource}/{id}/unarchive", responseDeserializer: content => content.ToLink());
        }
    }
}