//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookingSystemEF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Udlejning
    {
        public int UdlejningsId { get; set; }
        public string Status { get; set; }
        public int KundeId { get; set; }
        public int VærktøjId { get; set; }
        public System.DateTime FraDato { get; set; }
        public System.DateTime TilDato { get; set; }
    
        public virtual Kunde Kunde { get; set; }
        public virtual Værktøj Værktøj { get; set; }

        public double beregnFuldPris()
        {
            return Convert.ToDouble(this.udregnPeriode() * Værktøj.døgnpris);
        }

        private int udregnPeriode()
        {
            return (TilDato.Date - FraDato.Date).Days;
        }

    }
}
