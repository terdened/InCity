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
    
    public partial class EventPlace
    {
        public int EventId { get; set; }
        public int PlaceId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual Place Place { get; set; }
    }
}