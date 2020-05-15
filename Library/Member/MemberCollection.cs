namespace MemberService
{
    public class UserError : System.Exception
    {
        public UserError(string message) : base(message) { }
    }
    public class MemberCollection
    {
        private Member[] members = new Member[65536];
        private int memberCount = 0;

        private static int BinarySearch(Member[] arr, int l, int r, string x)
        {
            if (r >= l)
            {
                int mid = l + (r - l) / 2;
                if (arr[mid].UserName == x) return mid;
                if (arr[mid].UserName.CompareTo(x) == 1) return BinarySearch(arr, l, mid - 1, x);
                return BinarySearch(arr, mid + 1, r, x);
            }
            return -1;
        }

        public Member FindMember(string userName, string password)
        {
            var member = FindMember(userName);
            if (!member.CheckPassword(password))
                throw new UserError("Password does not match");

            return member;
        }

        public Member FindMember(string userName)
        {
            if (memberCount == 0)
                throw new UserError("User could not be found");

            int i = BinarySearch(members, 0, memberCount - 1, userName);
            if (i == -1)
                throw new UserError("User could not be found");

            return members[i];
        }

        // A single pass of the insertion sort on a for i. Moves the item at i to the position above
        // the first item less then it 
        private void Insertion(Member[] A, int i)
        {
            Member v = A[i];
            int j = i - 1;
            while (j >= 0 && A[j] > v)
                A[j + 1] = A[j--];

            A[j + 1] = v;
        }

        public void AddMember(Member member)
        {
            if (memberCount > 0 && BinarySearch(members, 0, memberCount - 1, member.UserName) != -1)
                throw new UserError("User already exists");

            members[memberCount] = member;
            Insertion(members, memberCount);
            memberCount++;
        }
    }
}
