﻿// MIT License
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

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Paymongo.Sharp.Core.Entities;
using Paymongo.Sharp.Core.Enums;

#pragma warning disable CS8618

namespace Paymongo.Sharp.Sources.Entities
{
    public class Source
    {
        [JsonIgnore]
        public string Id { get; set; }
        
        [JsonProperty("description",
            NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        
        [JsonProperty("amount",
            NullValueHandling = NullValueHandling.Ignore)]
        public long Amount { get; set; }
        
        [JsonProperty("currency",
            NullValueHandling = NullValueHandling.Ignore)]
        public Currency Currency { get; set; }
        
        [JsonProperty("billing",
            NullValueHandling = NullValueHandling.Ignore)]
        public Billing Billing { get; set; }
        
        [JsonProperty("type",
            NullValueHandling = NullValueHandling.Ignore)]
        public SourceType Type { get; set; }
        
        [JsonProperty("status",
            NullValueHandling = NullValueHandling.Ignore)]
        public SourceStatus Status { get; set; }

        [JsonProperty("redirect",
            NullValueHandling = NullValueHandling.Ignore)]
        public Redirect? Redirect { get; set; }
        
        [JsonProperty("livemode",
            NullValueHandling = NullValueHandling.Ignore)]
        public bool LiveMode { get; set; }
        
        [JsonProperty("created_at",
            NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }
        
        [JsonProperty("updated_at",
            NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }
        
    }
}