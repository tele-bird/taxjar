using System;
using System.Collections.Generic;

namespace TaxHelper.Common.Models
{
    public interface IBaseModel
    {
        IList<string> GetErrors();
    }
}
