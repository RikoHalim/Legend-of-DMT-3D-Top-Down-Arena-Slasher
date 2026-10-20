using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	[SerializeField] GameObject hero;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject soldier;
	[SerializeField] GameObject ranger;

	private Animator heroAnim;
	private Animator tankerAnim;
	private Animator soldierAnim;
	private Animator rangerAnim;
	private const string bylineText = "By Riko Halim SWE2404525";

	void Awake() {

		Assert.IsNotNull (hero);
		Assert.IsNotNull (tanker);
		Assert.IsNotNull (soldier);
		Assert.IsNotNull (ranger);

	}

	// Use this for initialization
	void Start () {
	
		heroAnim = hero.GetComponent<Animator> ();
		tankerAnim = tanker.GetComponent<Animator> ();
		soldierAnim = soldier.GetComponent<Animator> ();
		rangerAnim = ranger.GetComponent<Animator> ();

		AddByline ();
		StartCoroutine (showcase ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator showcase() {

		yield return new WaitForSeconds (1f);
		heroAnim.Play ("SpinAttack");
		yield return new WaitForSeconds (1f);
		tankerAnim.Play ("Attack");
		yield return new WaitForSeconds (1f);
		soldierAnim.Play ("Attack");
		yield return new WaitForSeconds (1f);
		rangerAnim.Play ("Attack");
		yield return new WaitForSeconds (1f);

		StartCoroutine (showcase ());
	}

	void AddByline() {

		Text titleText = null;
		Text[] texts = GameObject.FindObjectsOfType<Text> ();

		for (int i = 0; i < texts.Length; i++) {
			if (texts [i].gameObject.name == "Devslopes" || texts [i].text.Trim () == "DMT") {
				titleText = texts [i];
				break;
			}
		}

		if (titleText == null) {
			return;
		}

		titleText.text = "DMT";

		Transform bylineTransform = titleText.transform.parent.Find ("Byline");
		Text byline = bylineTransform != null ? bylineTransform.GetComponent<Text> () : null;

		if (byline == null) {
			GameObject bylineObject = new GameObject ("Byline", typeof(RectTransform));
			bylineObject.transform.SetParent (titleText.transform.parent, false);
			byline = bylineObject.AddComponent<Text> ();
		}

		byline.text = bylineText;
		byline.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		byline.fontSize = 16;
		byline.fontStyle = FontStyle.Normal;
		byline.alignment = TextAnchor.MiddleCenter;
		byline.color = titleText.color;
		byline.raycastTarget = false;
		byline.horizontalOverflow = HorizontalWrapMode.Overflow;
		byline.verticalOverflow = VerticalWrapMode.Overflow;

		Outline bylineOutline = byline.GetComponent<Outline> ();
		if (bylineOutline == null) {
			bylineOutline = byline.gameObject.AddComponent<Outline> ();
		}
		bylineOutline.effectColor = Color.white;
		bylineOutline.effectDistance = new Vector2 (1f, -1f);
		bylineOutline.useGraphicAlpha = true;

		RectTransform titleRect = titleText.GetComponent<RectTransform> ();
		RectTransform bylineRect = byline.GetComponent<RectTransform> ();
		bylineRect.SetParent (titleText.transform.parent, false);
		bylineRect.anchorMin = titleRect.anchorMin;
		bylineRect.anchorMax = titleRect.anchorMax;
		bylineRect.pivot = titleRect.pivot;
		bylineRect.sizeDelta = new Vector2 (520f, 32f);
		bylineRect.anchoredPosition = titleRect.anchoredPosition + new Vector2 (0f, -48f);
		bylineRect.localScale = Vector3.one;
		bylineRect.localRotation = Quaternion.identity;
	}

	public void Battle() {
		SceneManager.LoadScene ("Level");
	}

	public void Quit() {
		Application.Quit ();
	}
}