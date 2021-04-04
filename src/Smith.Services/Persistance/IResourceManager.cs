using System.Collections.Generic;
using Smith.Services.Persistance.Resources;

namespace Smith.Services.Persistance
{
    public interface IResourceManager
    {
        IList<PhoneCode> GetPhoneCodes();
    }
}
