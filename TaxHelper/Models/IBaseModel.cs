using System.Collections.Generic;

namespace TaxHelper.Models
{
    public interface IBaseModel
    {
        IList<string> GetErrors();
    }
}
