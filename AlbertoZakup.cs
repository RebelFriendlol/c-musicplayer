using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertoPlayer;
    public class AlbertoZakup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int IDzakupu { get; set; }

        public int IDuser { get; set; }
        public int IDsong { get; set; }
        public decimal CenaZakupu { get; set; }
        public DateTime DataZakupu { get; set; }
    }
