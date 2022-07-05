using UnityEngine;

namespace Pages.Quest
{
    public class ChapterList : MonoBehaviour
    {
        [SerializeField] 
        private Chapter[] _chapters;

        public void CloseAllChapters()
        {
            foreach (var chapter in _chapters) 
                chapter.Close();
        }
    }
}