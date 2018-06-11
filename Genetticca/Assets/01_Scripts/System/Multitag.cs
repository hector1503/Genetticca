using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Multitag : MonoBehaviour {
	
	private static List<Multitag> taggers = null;
	
	// Used only for inspector convenience, runtime interactions should be directed to the TagsSet HashSet
	[SerializeField]
    [TagSelector]
	private List<string> Tags;

	// Actual tags storage, since it'll be searched a lot
	public HashSet<string> TagsSet;
	
	void Awake() {
		if (taggers == null) {
			taggers = new List<Multitag>();
		}
		
		taggers.Add(this);
		TagsSet = new HashSet<string>(Tags);
	}
	
    public bool ContainsTag(string value)
    {
       
        foreach(string tag in this.Tags)
        {
            if (tag == value)
                return true;
        }
        return false;

    }

    public bool containsTagInList(string[] valueList)
    {
        bool _outValue = false;
        foreach(string value in valueList)
        {
            _outValue = ContainsTag(value);
            if (_outValue)
                return _outValue;
        }
        return _outValue;
    }

	public static IEnumerable<GameObject> FindGameObjectsWithTag(string tag) {
		if (taggers == null || tag == null) return null;
	
		return taggers.Where(x => x.TagsSet.Contains(tag)).Select(x => x.gameObject);
	}
	
	public static IEnumerable<GameObject> FindGameObjectsWithTags(IEnumerable<string> tags, bool loose = false) {
		if (taggers == null || tags == null) return null;
		
		return taggers
			.Where(x => loose ? x.TagsSet.Intersect(tags).Any() : x.TagsSet.Intersect(tags).Count() == tags.Count())
			.Select(x => x.gameObject);
	}
}
