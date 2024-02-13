using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Models
{
    // Identity User default degerlere ek deger eklemek icin //
    // Bu sekilde kullanýlmak istenilirse programda IdentityUser kullanilmayacak onun yerine AppUser kullanilacak
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}