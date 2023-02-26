using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>
{
	//delete tower
	public TowerBtn towerBtnPressed { get; set; }

	private SpriteRenderer spriteRenderer;

	private List<Tower> towerList = new List<Tower>();
	private List<Collider2D> BuildList = new List<Collider2D>();
	private Collider2D buildTitle;
	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		buildTitle = GetComponent<Collider2D>();
		spriteRenderer.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if (hit.collider.tag == "buildSite")
			{
				buildTitle = hit.collider;
				buildTitle.tag = "buildSiteFull";
				RegisterBuildSite(buildTitle);
				placeTower(hit);// hàm đặt trụ
			}
		}
		if (spriteRenderer.enabled)
		{
			followMouse();
		}
	}

	public void RegisterBuildSite(Collider2D buildTag)
	{
		BuildList.Add(buildTag);
	}

	public void RegisterTower(Tower tower)
	{
		towerList.Add(tower);
	}
	public void RenameTagsBuildSites()
	{
		foreach (Collider2D buildTag in BuildList)
		{
			buildTag.tag = "buildSite";
		}
		BuildList.Clear();
	}

	public void DestoyAllTower()
	{
		foreach(Tower tower in towerList)
		{
			Destroy(tower.gameObject);
		}
		towerList.Clear();
	}
	public void placeTower(RaycastHit2D hit)
	{
		if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
		{
			Tower newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
			buyTower(towerBtnPressed.TowerPrice);
			GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuilts);
			RegisterTower(newTower);
			disnableDragSprite();
		}
	}

	public void buyTower(int price)
	{
		GameManager.Instance.subtractMoney(price);
	}

	public void selectedTower(TowerBtn towerSelected)
	{
		if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney)
		{
			towerBtnPressed = towerSelected;
			enableDragSprite(towerBtnPressed.DragSprite);
		}
	}
	public void followMouse()
	{
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector2(transform.position.x, transform.position.y);
	}

	public void enableDragSprite(Sprite sprite)
	{
		spriteRenderer.enabled = true;
		spriteRenderer.sprite = sprite;
	}

	public void disnableDragSprite()
	{
		spriteRenderer.enabled = false;

	}
}
