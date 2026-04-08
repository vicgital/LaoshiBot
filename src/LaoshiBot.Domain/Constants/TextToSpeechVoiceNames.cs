namespace LaoshiBot.Domain.Constants
{
    public static class TextToSpeechVoiceNames
    {
        public static string GetRandomChineseVoiceName()
        {
            var chineseVoiceNames = new List<string>
            {
                "zh-CN-YunxiNeural",
                "zh-CN-XiaoxiaoNeural",
                "zh-CN-YunjianNeural",
                "zh-CN-YunyangNeural"
            };
            Random random = new();
            int index = random.Next(chineseVoiceNames.Count);
            return chineseVoiceNames[index];
        }


    }
}
