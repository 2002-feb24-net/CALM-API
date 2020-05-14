using System.Collections.Generic;

namespace Calm.Lib.Items
{
    public class GatheringItemOut
    {
        public string Title { get; set; }
        public string occurrenceData { get; set; }
        public string City { get; set; }
        public string details { get; set; }
        public UserItem organizer { get; set; }
        public List<UserItem> atendees { get; set; }
        public bool isEvent { get; set; }
    }
}
