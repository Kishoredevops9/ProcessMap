namespace EKS.ProcessMaps.Models
{
    /// <summary>
    /// Model class of user preferences
    /// </summary>
    public class UserPreferencesModel
    {
        /// Id
        public int Id { get; set; }

        /// UserIdentifier
        public string UserIdentifier { get; set; }

        /// Tiles
        public string Tiles { get; set; }
    }
}
