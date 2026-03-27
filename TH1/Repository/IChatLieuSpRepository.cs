using TH1.Models;
namespace TH1.Repository
{
    public interface IChatLieuSpRepository
    {
        TChatLieu Add(TChatLieu chatLieu);

        TChatLieu Update(TChatLieu chatLieu);

        TChatLieu Delete(string maChatLieu);

        TChatLieu  GetChatLieu(string maChatLieu);

        IEnumerable<TChatLieu> GetAllChatLieus();
    }
}
