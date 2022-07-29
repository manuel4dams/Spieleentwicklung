namespace Project
{
    public class PrefsHandlerBool : PrefsHandlerAbstract<bool>
    {
        public bool invert;
        protected override bool GetPrefs(Prefs prefs)
        {
            return invert ^ prefs.GetBool();
        }

        protected override void SetPrefs(Prefs prefs, bool value)
        {
            prefs.Set(invert ^ value);
        }
    }
}
