using TH1.Models;

namespace TH1.Repository
{
    public class ChatLieuSpRepository : IChatLieuSpRepository
    {
        private readonly QlbanVaLiContext _context;
        public ChatLieuSpRepository(QlbanVaLiContext context)
        {
            _context = context;
        }
        public TChatLieu Add(TChatLieu chatLieu)
        {
            _context.TChatLieus.Add(chatLieu);
            _context.SaveChanges();
            return chatLieu;
        }

        public TChatLieu Delete(string maChatLieu)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TChatLieu> GetAllChatLieus()
        {
            return _context.TChatLieus.ToList();
        }

        public TChatLieu GetChatLieu(string maChatLieu)
        {
            return _context.TChatLieus.Find(maChatLieu);
        }

        public TChatLieu Update(TChatLieu chatLieu)
        {
            _context.TChatLieus.Update(chatLieu);
            _context.SaveChanges();
            return chatLieu;
        }
    }
}
