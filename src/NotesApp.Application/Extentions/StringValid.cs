namespace NotesApp.Application.Extentions
{
    public static class StringValid
    {
        public static bool IsValid(this string value, int maxLength) 
        {
            if(string.IsNullOrWhiteSpace(value) || value.Length > maxLength) return false;
            return true;
        }
    }
}