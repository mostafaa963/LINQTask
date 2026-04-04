using System;
using System.Collections.Generic;

namespace LINQTask.Model;

public partial class ProductPrint
{
    public string Fullname { get; set; } = null!;

    public int OrderId { get; set; }

    public byte OrderStatus { get; set; }

    public DateOnly OrderDate { get; set; }
}
