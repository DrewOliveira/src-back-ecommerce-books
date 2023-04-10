using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Enums
{
    public enum StatusOrder
    {
        PROCESSING = 1,
        APPROVED = 2,
        FAILED  = 3,
        TRANSPORT = 4,
        DELIVERED = 5,
        REPLACEMENT = 6,
        CHANGED = 7
    }
}
