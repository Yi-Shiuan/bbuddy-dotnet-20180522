using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace GOOS_Sample.Models
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string YearMonth { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}