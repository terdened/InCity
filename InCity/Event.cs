//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InCity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public Event()
        {
            this.EventPlace = new HashSet<EventPlace>();
            this.EventTag = new HashSet<EventTag>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> PosterId { get; set; }
    
        public virtual Pictures Pictures { get; set; }
        public virtual ICollection<EventPlace> EventPlace { get; set; }
        public virtual ICollection<EventTag> EventTag { get; set; }
    }
}
