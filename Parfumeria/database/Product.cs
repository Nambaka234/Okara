using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;

namespace Parfumeria.database;

public partial class Product
{
    public string Productarticlenumber { get; set; } = null!;

    public string Productname { get; set; } = null!;

    public string Productdescription { get; set; } = null!;

    public string Productcategory { get; set; } = null!;

    public string Productphoto { get; set; } = null!;

    public string Productmanufacturer { get; set; } = null!;

    public decimal Productcost { get; set; }

    public short? Productdiscountamount { get; set; }

    public int Productquantityinstock { get; set; }

    public string Productstatus { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public Bitmap Mainimage
    {
        get
        {
            try
            {
                return new Bitmap("." + Productphoto);
            }
            catch (System.Exception)
            {
                return new Bitmap("./products/picture.png");
            }
        }
    }
}
