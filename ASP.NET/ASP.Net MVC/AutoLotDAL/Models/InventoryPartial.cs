using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLotDAL.Models
{
    public partial class Inventory
    {
        public override string ToString()
        {
            // Since the PetName column could be empty, supply
            // the default name of **No Name**.
            return $"{this.PetName ?? "**No Name**"} is a {this.Color} {this.Make} with ID {this.Id}.";
            //return $"{this.PetName ?? "**No Name**"} is a {this.Color} {this.Make} with ID {this.CarId}.";
        }

        // Add a calculated field that combines the Make and Color of the car. 
        // This is a field that is not to be stored in the database 
        // or populated when an object is materialized from the data reader.
        // The[NotMapped] attribute informs EF that this is a.NET-only property

        [NotMapped]
        public string MakeColor => $"{Make} + ({Color})";
    }
}
