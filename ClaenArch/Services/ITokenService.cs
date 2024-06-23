using Domains.Data;

namespace ClaenArch.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(TblUsers user);
    }
}
