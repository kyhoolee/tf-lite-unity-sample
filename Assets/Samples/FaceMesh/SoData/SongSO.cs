using UnityEngine;

[CreateAssetMenu]
public class SongSO : ScriptableObject
{
    [SerializeField] private string _nameSong;
    [SerializeField] private string _author;
    [SerializeField] private float _time;
    [SerializeField] private string _path;
    public string NameSong { get => _nameSong; set { _nameSong = value; } }
    public string Author { get => _author; set { _author = value; } }
    public float Time { get => _time; set { _time = value; } }
    public string Path { get => _path; set { _path = value; } }

    public void SetSong(string ns, string a, float t, string p)
    {
        _nameSong = ns;
        _author = a;
        _time = t;
        _path = p;
    }
}
