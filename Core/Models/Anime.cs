
namespace Core.Models
{
    public class Anime
    {
        public const int Max_L_Title = 100;
        public const int Max_L_Genre = 35;
        public const int Min_Rating = 0;
        public const int Max_Rating = 10;

        private enum Statuses

        {
            Planned,
            Watching,
            Completed,
            Dropped
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public double Rating { get; private set; }
        public string Status => _status.ToString();

        private Statuses _status;
        public List<string> Genres { get; private set; } = new List<string>();

        private Anime(Guid id, string title, double rating, Statuses status, List<string> genres)
        {
            Id = id;
            Title = title;
            Rating = rating;
            _status = status;
            Genres = genres;
        }

        public static (Anime? anime, string Errors) Create (Guid id, string title,  string status, double rating, List<string> genres)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(title) || title.Length > Max_L_Title) {  errors.Add($"Title cannot be empty or longer than {Max_L_Title} characters.");
            }

            if (rating < Min_Rating || rating > Max_Rating) {  errors.Add($"Rating must be between {Min_Rating} and {Max_Rating}.");
            }

            if(genres == null || !genres.Any()) {  errors.Add("At least one genre is required.");
            }

            if (genres != null)
            {
                foreach (var genre in genres)
                {
                    if (string.IsNullOrEmpty(genre) || genre.Length > Max_L_Genre)
                        errors.Add($"Genre cannot be empty or longer than {Max_L_Genre} characters.");
                }
            }

            if(!Enum.TryParse<Statuses>(status, true, out var status_v))
            {
                errors.Add($"Invalid status. Allowed values: {string.Join(", ", Enum.GetNames(typeof(Statuses)))}");
            }

            if (errors.Any())
            {
                return (null, string.Join(Environment.NewLine, errors));
            }

            var anime = new Anime(id, title.Trim(), rating, status_v, genres);
            return (anime,  string.Empty);
        }

        public void Update(string title, string status, double rating, List<string> genres)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(title) || title.Length > Max_L_Title)
            {
                errors.Add($"Title cannot be empty or longer than {Max_L_Title} characters.");
            }

            if (rating < Min_Rating || rating > Max_Rating)
            {
                errors.Add($"Rating must be between {Min_Rating} and {Max_Rating}.");
            }

            if (genres == null || !genres.Any())
            {
                errors.Add("At least one genre is required.");
            }

            if (genres != null)
            {
                foreach (var genre in genres)
                {
                    if (string.IsNullOrEmpty(genre) || genre.Length > Max_L_Genre)
                        errors.Add($"Genre cannot be empty or longer than {Max_L_Genre} characters.");
                }
            }

            if (!Enum.TryParse<Statuses>(status, true, out var status_v))
            {
                errors.Add($"Invalid status. Allowed values: {string.Join(", ", Enum.GetNames(typeof(Statuses)))}");
            }

            if (errors.Any())
                throw new ArgumentException(string.Join(Environment.NewLine, errors));

            Title = title;
            _status = status_v;
            Rating = rating;
            Genres = genres;
        }

    }
}
