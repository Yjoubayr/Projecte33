namespace gridfsapi
{
    public class AlbumUpload
    {
        public int Any { get; set; }
        public string Titol { get; set; }
        public string Genere { get; set; }
        public string UIDSong { get; set; }
        public IFormFile ImatgePortada { get; set; }
        public IFormFile ImatgeContraPortada { get; set; }
    }
}