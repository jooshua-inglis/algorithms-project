using System;
namespace MovieService
{
    public class MovieError : System.Exception
    {
        public MovieError(string message) : base(message) { }
    }

    class MovieNode
    {
        public string Key { get { return Value.Title.ToLower(); } }
        public Movie Value { get; set; }
        public MovieNode left, right;

        public MovieNode(Movie movie)
        {
            Value = movie;
        }
    }

    public class MovieCollection
    {
        private MovieNode root;
        private int nodeCount;
        public int CountUnique { get { return nodeCount; } }


        public Movie FindMovie(string title)
        {
            var result = Search(title.ToLower(), root);
            return result != null ? result.Value : null;
        }

        public void AddMovie(Movie movie)
        {
            Insert(movie, ref root);
        }

        public void DeleteMovie(string title)
        {
            bool itemDeleted = false;
            root = Delete(title.ToLower(), root, ref itemDeleted);
            if (itemDeleted)
                nodeCount--;
        }

        private MovieNode Search(string title, MovieNode node)
        {
            if (node == null)
                return null;
            else if (title.CompareTo(node.Key) == 0)
                return node;

            return Search(title, title.CompareTo(node.Key) == 1 ? node.right : node.left);
        }

        private void Insert(Movie movie, ref MovieNode node)
        {
            if (node == null)
            {
                node = new MovieNode(movie);
                nodeCount++;
            }
            else if (movie.Key == node.Key)
            {
                node.Value.Count += movie.Count;
                if (movie.Avaliable != null && node.Value.Avaliable != null)
                    node.Value.Avaliable += movie.Avaliable;
            }
            else if (movie.Key.CompareTo(node.Key) == 1)
                Insert(movie, ref node.right);
            else
                Insert(movie, ref node.left);
        }

        private MovieNode RightMost(MovieNode node)
        {
            if (node.right != null)
                return RightMost(node.right);
            else
                return node;
        }

        private MovieNode Delete(string title, MovieNode node, ref bool itemDeleted)
        {
            if (node == null) return node;
            var compare = title.CompareTo(node.Key);

            if (compare == 1)
                node.right = Delete(title, node.right, ref itemDeleted);
            else if (compare == -1)
                node.left = Delete(title, node.left, ref itemDeleted);
            else
            {
                itemDeleted = true;
                if (node.Value.Count > 1)
                {
                    node.Value.Count--;
                    return node;
                }
                if (node.left != null && node.right != null)
                {
                    node.Value = RightMost(node.left).Value;
                    node.left = Delete(node.Value.Key, node.left, ref itemDeleted);
                }
                else if (node.left != null)
                    return node.left;
                else if (node.right != null)
                    return node.right;
                else
                    return null;
            }
            return node;
        }

        public void PrintInorder() { PrintInorder(root); }

        private void PrintInorder(MovieNode node)
        {
            if (node == null)
                return;
            PrintInorder(node.left);

            Console.WriteLine(node.Value.ToString());
            Console.WriteLine(new string('=', 30));

            PrintInorder(node.right);
        }

        private void GetBorrowCountArray(MovieNode node, Movie[] A, ref int i)
        {
            if (node == null)
                return;
            GetBorrowCountArray(node.left, A, ref i);
            A[i++] = node.Value;
            GetBorrowCountArray(node.right, A, ref i);
        }

        public Movie[] AsArray()
        {
            int i = 0;
            var movieArray = new Movie[nodeCount];

            GetBorrowCountArray(root, movieArray, ref i);
            return movieArray;
        }
    }
}
