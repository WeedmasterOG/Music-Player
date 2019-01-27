using System;
using Newtonsoft.Json;

namespace CodeShare
{
    class DataFormatter
    {
        public FormattedPasteBinData DeserializePasteBin(string input)
        {
            return JsonConvert.DeserializeObject<FormattedPasteBinData>(input);
        }

        public FormattedSettingsData DeserializeSettings(string input)
        {
            return JsonConvert.DeserializeObject<FormattedSettingsData>(input);
        }

        public class FormattedPasteBinData
        {
            public string Command { get; set; }
            public string AudioSource { get; set; }
            public int Volume { get; set; }
            public int StartPosition { get; set; }
            public int Delay { get; set; }
            public string Id { get; set; }
        }

        public class FormattedSettingsData
        {
            public string PastebinURL { get; set; }
            public int CheckingInterval { get; set; }
        }
    }
}
