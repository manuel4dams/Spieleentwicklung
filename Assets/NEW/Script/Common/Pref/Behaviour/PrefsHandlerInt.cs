namespace Project
{
    public class PrefsHandlerInt : PrefsHandlerAbstract<int>
    {
        protected override int GetPrefs(Prefs prefs)
        {
            return prefs.GetInt();
        }

        protected override void SetPrefs(Prefs prefs, int value)
        {
            prefs.Set(value);
        }
    }
}
