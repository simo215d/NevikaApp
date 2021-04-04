using SQLite;

namespace NevikaApp.Data
{
	public class VeggieModel
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DanishName { get; set; }
        public string EnglishName { get; set; }

        /// <summary>
        /// Weather the user has selected this allergen to keep track of
        /// </summary>
        public bool Selected { get; set; } = false;
    }
}
