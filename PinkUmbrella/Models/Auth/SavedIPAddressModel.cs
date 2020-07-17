using System.ComponentModel.DataAnnotations.Schema;
using Poncho.Models.Auth;

namespace PinkUmbrella.Models.Auth
{
    public class SavedIPAddressModel: IPAddressModel
    {
        public long Id { get; set; }
        
        [NotMapped]
        public IPBlockModel Block { get; set; }
    }
}