using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour {

	private static GameHandler instance;
	public static GameHandler Instance{
		get{
			return instance;
		}
	}
	[Header("Prefab Reference")]
	public GameObject dragOb;
	public GameObject levelSelectorOb;
	public GameObject worldSelectorOb;
	public GameObject comboShowOb;
	[Header("UI Reference")]
	public GameObject splashScreen;
	public GameObject menuScreen;
	public GameObject gameScreen;
	public GameObject closePopUp;
	public GameObject resumeGame;
	public GameObject exitGame;
	public Image timeSlider;
	public TextMeshProUGUI goldCount;
	public TextMeshProUGUI selectorPopUpLevel;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI countdownText;
	public PopUpController infoWindow;
	public EndGameStats endGameStats;
	[Header("Shape Reference")]
	public List<Sprite> shapes = new List<Sprite>();
	[Header("Game Holder Reference")]
	public Transform spawnHolder;
	public Transform gameHolder;
	public Transform worldButtonHolder, levelButtonHolder;
	public RectTransform worldButtonGroup, levelButtonGroup;
	public RectTransform scoreBar;
	public Stack<GameObject> boxes = new Stack<GameObject>();
	public List<GameObject> boxesInPlay = new List<GameObject> ();
	public Stack<GameObject> comboShows = new Stack<GameObject> ();
	public TextMeshProUGUI timerText;
	[HideInInspector] public bool boss1 = false;
	List<Transform> spawnPoints = new List<Transform>();
	[Header("Spawning Areas")]
	public List<DropArea> dropAreas = new List<DropArea>();
	[HideInInspector] public List<bool> spawnBool = new List<bool>();
	[HideInInspector] public List<int> possibleSpawns = new List<int>();
	[HideInInspector] public int levelAmt = 1;
	[HideInInspector] public int worldAmt = 1;
	[HideInInspector] public int currentLevel;
	[HideInInspector] public int score = 0;
	[HideInInspector] public int boxesDone = 0;
	[HideInInspector] public int comboInARow = 0;
	[HideInInspector] public List<int> levelBoxes = new List<int>();
	List<float> levelSpawnRate = new List<float>();
	List<LevelSelector> levelSelectors = new List<LevelSelector>();
	List<LevelSelector> worldSelectors = new List<LevelSelector>();
	[HideInInspector] public List<int> levelstar1 = new List<int>();
	[HideInInspector] public List<int> levelstar2 = new List<int>();
	[HideInInspector] public List<int> levelstar3 = new List<int>();
	[HideInInspector] public List<int> levelTimers = new List<int>();
	[HideInInspector] public List<int> worldNumber = new List<int>();
	List<bool> isBoss = new List<bool>();
	Dictionary<int, int[]> usedDropAreas = new Dictionary<int, int[]>();
	Dictionary<int, int[]> usedShapes = new Dictionary<int, int[]>();
	PopUpController currentPopUp;
	[HideInInspector] public int xBoxCounter = 0;
	[HideInInspector] public int bossHealth = 0;
	public bool choosingLevels = false;

	void Start(){
		instance = this;
		Application.targetFrameRate = 60;
		//Level Data
		worldAmt = 8;
		levelAmt = 11;
		//Level 1 Data
		levelBoxes.Add (15);                   //How many boxes will spawn this level
		levelSpawnRate.Add (.5f);              //Box spawn rate
		levelstar1.Add (1000);                  //Score needed for 1 stars
		levelstar2.Add (2000);                  //Score needed for 2 stars
		levelstar3.Add (3000);                  //Score needed for 3 stars
		levelTimers.Add (60);                  //How long the level lasts for
		worldNumber.Add(1);
		usedDropAreas.Add (1, new int[2]{0,1});//Used to activate certain drop areas
		usedShapes.Add (1, new int[2]{1,0});   //Determines what shapes are used for this stage
		isBoss.Add (false);                    //Is this a boss stage?

		//Level 2 Data
		levelBoxes.Add (25);
		levelSpawnRate.Add (.4f);
		levelstar1.Add (500);
		levelstar2.Add (1000);
		levelstar3.Add (1500);
		levelTimers.Add (60);
		worldNumber.Add(1);
		usedDropAreas.Add (2, new int[2]{0,1});
		usedShapes.Add (2, new int[2]{1,0});
		isBoss.Add (false); 

		//Level 3 Data
		levelBoxes.Add (30);
		levelSpawnRate.Add (.4f);
		levelstar1.Add (1000);
		levelstar2.Add (1500);
		levelstar3.Add (2000);
		levelTimers.Add (60);
		worldNumber.Add(1);
		usedDropAreas.Add (3, new int[2]{0,1});
		usedShapes.Add (3, new int[2]{1,2});
		isBoss.Add (false); 

		//Level 4 Data
		levelBoxes.Add (20);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (1000);
		levelstar2.Add (1500);
		levelstar3.Add (2000);
		levelTimers.Add (30);
		worldNumber.Add(1);
		usedDropAreas.Add (4, new int[2]{0,1});
		usedShapes.Add (4, new int[2]{2,1});
		isBoss.Add (false); 

		//Level 5 Data
		levelBoxes.Add (25);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (1500);
		levelstar2.Add (2000);
		levelstar3.Add (2500);
		levelTimers.Add (60);
		worldNumber.Add(2);
		usedDropAreas.Add (5, new int[2]{0,1});
		usedShapes.Add (5, new int[2]{2,3});
		isBoss.Add (false); 

		//Level 6 Data
		levelBoxes.Add (27);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (2000);
		levelstar2.Add (2500);
		levelstar3.Add (3000);
		levelTimers.Add (60);
		worldNumber.Add(3);
		usedDropAreas.Add (6, new int[2]{0,1});
		usedShapes.Add (6, new int[2]{0,3});
		isBoss.Add (false); 

		//Level 7 Data
		levelBoxes.Add (30);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (2500);
		levelstar2.Add (3000);
		levelstar3.Add (3500);
		levelTimers.Add (60);
		worldNumber.Add(4);
		usedDropAreas.Add (7, new int[3]{0,4,5});
		usedShapes.Add (7, new int[3]{1,0,2});
		isBoss.Add (false); 

		//Level 8 Data
		levelBoxes.Add (35);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (3000);
		levelstar2.Add (3500);
		levelstar3.Add (4000);
		levelTimers.Add (60);
		worldNumber.Add(5);
		usedDropAreas.Add (8, new int[3]{0,4,5});
		usedShapes.Add (8, new int[3]{3,1,20});
		isBoss.Add (false); 

		//Level 9 Data
		levelBoxes.Add (35);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (3000);
		levelstar2.Add (3500);
		levelstar3.Add (4000);
		levelTimers.Add (60);
		worldNumber.Add(6);
		usedDropAreas.Add (9, new int[4]{2,3,4,5});
		usedShapes.Add (9, new int[4]{1,0,2,3});
		isBoss.Add (false); 

		//Level 10 Data
		levelBoxes.Add (40);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (4000);
		levelstar2.Add (4500);
		levelstar3.Add (5000);
		levelTimers.Add (60);
		worldNumber.Add(7);
		usedDropAreas.Add (10, new int[4]{2,3,4,5});
		usedShapes.Add (10, new int[4]{3,2,0,1});
		isBoss.Add (false); 

		//Level 11 Boss Data
		levelBoxes.Add (999);
		levelSpawnRate.Add (.5f);
		levelstar1.Add (4000);
		levelstar2.Add (4500);
		levelstar3.Add (5000);
		levelTimers.Add (60);
		worldNumber.Add(8);
		usedDropAreas.Add (11, new int[4]{9,10,11,12});
		usedShapes.Add (11, new int[4]{0,1,2,4});
		isBoss.Add (true); 



		//Spawn boxes for pulling
		for (int i=0; i<50; i++) {
			GameObject temp = Instantiate (dragOb);
			temp.GetComponent<RectTransform>().SetParent(gameHolder);
			temp.SetActive(false);
			boxes.Push (temp);
		}
		//Combo prefabs
		for (int i=0; i<10; i++) {
			GameObject temp = Instantiate (comboShowOb);
			temp.GetComponent<RectTransform>().SetParent(gameHolder);
			temp.SetActive(false);
			comboShows.Push (temp);
		}
		int count = 0;
		//Counts up spawn points and dictionary for spawn boolean handling
		foreach (Transform child in spawnHolder) {
			spawnPoints.Add (child);
			spawnBool.Add (false);
			possibleSpawns.Add (count);
			count++;
		}
		goldCount.text = PlayerPrefs.GetInt ("coins").ToString ();

		SetupWorldSelectors ();
	}

	IEnumerator SetupGameRoutine(){
		boss1 = false;
		xBoxCounter = 0;
		bossHealth = 100;
		timerText.text = levelTimers [currentLevel - 1].ToString();
		timerText.gameObject.SetActive (false);
		score = 0;
		comboInARow = 0;
		AddScore (0);
		float posy = 0;
		timeSlider.fillAmount = 0;
		int y = 0;
		for(int i=0;i<dropAreas.Count;i++) {
			dropAreas[i].dropNum = -1;
			dropAreas[i].gameObject.SetActive(false);
			foreach(int x in usedDropAreas[currentLevel]){
				if(x==i){
					dropAreas[i].gameObject.SetActive(true);
					dropAreas[i].SetupDropArea(usedShapes[currentLevel][y], shapes[usedShapes[currentLevel][y]]);
					y++;
				}
			}
		}
		scoreBar.anchoredPosition = new Vector2(0,posy);
		for(float i=.25f;i>0;i-=Time.deltaTime){
			posy = Mathf.SmoothStep(393,460,i/.25f);
			scoreBar.anchoredPosition = new Vector2(0,posy);
			yield return null;
		}
		scoreBar.anchoredPosition = new Vector2(0,393);
		for (float i=0; i<2f; i+=Time.deltaTime) {
			timeSlider.fillAmount = Mathf.SmoothStep(0,1,Mathf.SmoothStep(0,1,i/2f));
			yield return null;
		}
		timeSlider.fillAmount = 1;
		countdownText.gameObject.SetActive (true);
		for (float i=3.99f; i>0; i-=Time.deltaTime) {
			if(i>1){
				countdownText.text = Mathf.Floor (i).ToString ();
			}else{
				countdownText.text = "GO!";
			}
			yield return null;
		}
		if (currentLevel == 11) {
			boss1 = true;
		}
		countdownText.text = "";
		countdownText.gameObject.SetActive (false);
		StartCoroutine ("NormalGameRoutine");
	}

	/// <summary>
	/// Routine for gameplay (spawning boxes, giving points, all that good stuff)
	/// </summary>
	IEnumerator NormalGameRoutine(){
		timerText.gameObject.SetActive (true);
		int i = 0;
		boxesDone = 0;
		comboInARow = 0;
		float counter = 0, tempTimer = levelTimers[currentLevel-1];
		while(tempTimer>0){
			counter+=Time.deltaTime;
			tempTimer-=Time.deltaTime;
			//Handle timer stuff!
			if(tempTimer>0){
				if(tempTimer/levelTimers[currentLevel-1]<=0){
					timeSlider.fillAmount = 0;
				}else{
					timeSlider.fillAmount = tempTimer/levelTimers[currentLevel-1];
				}
			}
			timerText.text = tempTimer.ToString ("F1");
			//Spawns the boxes to be used!
			if(possibleSpawns.Count>0&&counter>=levelSpawnRate[currentLevel-1]&&i<levelBoxes[currentLevel-1]){
				int rnd = Random.Range (0,possibleSpawns.Count);
				int rnd2=0;
				if (boss1) {
					rnd2 = Random.Range (0, usedShapes [currentLevel].Length-1);
				} else {
					rnd2 = Random.Range (0, usedShapes [currentLevel].Length);
				}
				GameObject temp = boxes.Pop ();
				boxesInPlay.Add (temp);
				temp.GetComponent<RectTransform>().anchoredPosition = spawnPoints[possibleSpawns[rnd]].GetComponent<RectTransform>().anchoredPosition;
				temp.GetComponent<DraggableObject>().grabbed = false;
				temp.GetComponent<DraggableObject>().spawnPoint = possibleSpawns[rnd];
				if (xBoxCounter >= 3) {
					temp.GetComponent<DraggableObject> ().dropNum = usedShapes [currentLevel] [3];
					temp.GetComponent<Image> ().sprite = shapes [usedShapes [currentLevel] [3]];
					xBoxCounter = 0;
				} else {
					temp.GetComponent<DraggableObject> ().dropNum = usedShapes [currentLevel] [rnd2];
					temp.GetComponent<Image> ().sprite = shapes [usedShapes [currentLevel] [rnd2]];
				}
				spawnBool[possibleSpawns[rnd]] = true;
				possibleSpawns.RemoveAt(rnd);
				temp.SetActive(true);
				counter = 0;
				i++;
			}
			yield return null;
		}
		LastBox ();
	}

	public void LastBox(){
		StopCoroutine("NormalGameRoutine");
		StartCoroutine ("AddTimeToScore");
	}

	IEnumerator AddTimeToScore(){
		yield return new WaitForSeconds (.5f);
		float newTime =  float.Parse(timerText.text)/20f;
		float adjustTime = newTime;
		for (float i = 0; i < newTime; i += .02f) {
			score += 50;
			adjustTime -= .02f;
			if (adjustTime <= 0) {
				adjustTime = 0;
			}
			timeSlider.fillAmount = (adjustTime * 20) / levelTimers [currentLevel - 1];
			timerText.text = (adjustTime * 20).ToString ("F0");
			scoreText.text = score.ToString ();
			yield return null;
		}
		bool star1Achieved = false, star2Achieved = false, star3Achieved = false;
		if (score >= levelstar1 [currentLevel - 1]) {
			star1Achieved = true;
		}
		if (score >= levelstar2 [currentLevel - 1]) {
			star2Achieved = true;
		}
		if (score >= levelstar3 [currentLevel - 1]) {
			star3Achieved = true;
		}
		endGameStats.SetupEndStats (star1Achieved,star2Achieved,star3Achieved);
	}

	public void FinishGame(){
		StopCoroutine ("SetupGameRoutine");
		StopCoroutine ("NormalGameRoutine");
		for (int i = boxesInPlay.Count-1;i>=0;i--) {
			if(boxesInPlay[i].activeSelf){
				boxesInPlay[i].GetComponent<DraggableObject>().SendDraggableBack(false);
			}
		}
		resumeGame.SetActive (false);
		exitGame.SetActive (false);
		countdownText.text = "";
		countdownText.gameObject.SetActive (false);
		GoToMainMenu ();
	}

	//Switches to main menu screen
	public void GoToMainMenu(){
		menuScreen.SetActive (true);
		gameScreen.SetActive (false);
		splashScreen.SetActive (false);
	}

	public void ShowComboMsg(Vector2 pos, bool good){
		GameObject popped = comboShows.Pop ();
		popped.GetComponent<RectTransform> ().anchoredPosition = pos;
		popped.SetActive (true);
		popped.GetComponent<ComboShow> ().ShowComboText (good);
	}

	void SetupWorldSelectors(){
		Vector2 pos = new Vector2(0,-73);
		for(int i=0;i<worldAmt;i++){
			GameObject temp = Instantiate(worldSelectorOb);
			temp.GetComponent<RectTransform>().SetParent(worldButtonHolder);
			temp.GetComponent<RectTransform>().anchoredPosition = pos;
			worldSelectors.Add(temp.GetComponent<LevelSelector>());
			worldSelectors [i].SetupSelector (i + 1, i + 1, true);
			pos.y -= 146;
		}
		worldButtonHolder.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, (pos.y*-1));
	}

	public void SetupLevelSelectors(int world){
		choosingLevels = true;
		Vector2 pos = new Vector2(0,-73);
		int levels = 0;
		for(int i=0;i<levelAmt;i++){
			if (worldNumber[i] == world) {
				GameObject temp = Instantiate (levelSelectorOb);
				temp.GetComponent<RectTransform> ().SetParent (levelButtonHolder);
				temp.GetComponent<RectTransform> ().anchoredPosition = pos;
				levelSelectors.Add (temp.GetComponent<LevelSelector> ());
				levels++;
				levelSelectors [levels-1].SetupSelector (i + 1, levels, false);
				pos.y -= 146;
			}
		}
		levelButtonHolder.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, (146*levels));
		StartCoroutine ("AnimateSelectors");
	}

	public void BackToWorldButtons(){
		choosingLevels = false;
		StartCoroutine ("AnimateSelectors");
	}

	IEnumerator AnimateSelectors(){
		float moveAmt = 550f;
		if (choosingLevels) {
			moveAmt = -550f;
		}
		float worldOb = worldButtonGroup.anchoredPosition.x;
		float levelOb = levelButtonGroup.GetComponent<RectTransform> ().anchoredPosition.x;
		float lerp = 0;
		for (float i = 0; i < .3f; i += Time.deltaTime) {
			lerp = Mathf.SmoothStep (worldOb, worldOb + moveAmt, i/.3f);
			worldButtonGroup.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (lerp, 0);
			lerp = Mathf.SmoothStep (levelOb, levelOb + moveAmt, i/.3f);
			levelButtonGroup.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (lerp, 0);
			yield return null;
		}
		worldButtonGroup.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (worldOb + moveAmt, 0);
		levelButtonGroup.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (levelOb + moveAmt, 0);
		if (!choosingLevels) {
			for (int i = levelSelectors.Count-1; i > -1; i--) {
				Destroy (levelSelectors [i].gameObject);
				levelSelectors.RemoveAt (i);
			}
		}
	}


	public void LevelSelected(int level, int displayLevel){
		currentLevel = level;
		selectorPopUpLevel.text = "Level " + displayLevel.ToString ();
		StartPopUp (infoWindow);
	}

	public void StartPopUp(PopUpController popUp){
		popUp.EnablePopUp ();
		closePopUp.SetActive (true);
		currentPopUp = popUp;
	}

	public void ExitPopUp(){
		Time.timeScale = 1;
		if (currentPopUp != null) {
			currentPopUp.DisablePopUp ();
			currentPopUp = null;
		}
	}

	public void AddCoins(int add){
		int temp = PlayerPrefs.GetInt ("coins");
		temp += add;
		PlayerPrefs.SetInt ("coins", temp);
		goldCount.text = temp.ToString ();
	}

	public void AddScore(int add){
		score += add;
		scoreText.text = score.ToString ();
	}

	public void PlayLevel(){
		ExitPopUp ();
		resumeGame.SetActive (true);
		exitGame.SetActive (true);
		menuScreen.SetActive (false);
		gameScreen.SetActive (true);
		splashScreen.SetActive (false);
		StartCoroutine ("SetupGameRoutine");
	}

	public IEnumerator DamageBoss(){
		RectTransform rect = dropAreas [12].GetComponent<RectTransform> ();
		Image image = dropAreas [12].GetComponent<Image> ();
		float xPos = 0;
		Color colorL = Color.white;
		for (float i = 0; i < 1f; i += Time.deltaTime) {
			colorL = Color.Lerp (Color.red, Color.white, Mathf.SmoothStep(0,1,i/1f));
			xPos = ((Mathf.Sin (i*40)*50)-25)*(1-i);
			rect.anchoredPosition = new Vector2 (xPos, rect.anchoredPosition.y);
			image.color = colorL;
			yield return null;
		}
		rect.anchoredPosition = new Vector2 (0, rect.anchoredPosition.y);
		image.color = Color.white;
	}

}