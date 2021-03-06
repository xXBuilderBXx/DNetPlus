﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.API
{
    internal class InteractionComponent_Json
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("style")]
        public Optional<ComponentButtonType> Style { get; set; }
        [JsonProperty("label")]
        public Optional<string> Label { get; set; }
        [JsonProperty("emoji")]
        public Optional<EmojiJson> Emoji { get; set; }
        [JsonProperty("custom_id")]
        public Optional<string> Id { get; set; }
        [JsonProperty("url")]
        public Optional<string> Url { get; set; }
        [JsonProperty("disabled")]
        public Optional<bool> Disabled { get; set; }
        [JsonProperty("components")]
        public Optional<InteractionComponent_Json[]> Components { get; set; }
    }
}
