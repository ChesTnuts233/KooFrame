// using GridSystem;
// using System.Collections;
// using UnityEngine;
//
//
// public class Monster : MonoBehaviour
// {
// 	public float attackDuration = 1f;  //怪物的攻击间隔时间
// 	private bool isAttacking;
// 	private Animator MonsterAni;
// 	private SpriteRenderer spriteRenderer;
// 	public Attack3x3 attack;
// 	public bool isBanana;
//
// 	private void Awake()
// 	{
// 		MonsterAni = GetComponent<Animator>();
// 		spriteRenderer = GetComponent<SpriteRenderer>();
// 	}
//
// 	private void OnTriggerEnter2D(Collider2D col)
// 	{
// 		if (!col.CompareTag("Player")) return;
// 		if (!isAttacking)
// 		{
// 			isAttacking = true;
// 			StartCoroutine(Attack3x3ToPlayer());
// 		}
// 	}
//
// 	private void OnTriggerExit2D(Collider2D collision)
// 	{
// 		if (!collision.CompareTag("Player")) return;
// 		//StopCoroutine(AttackToPlayer());
// 		isAttacking = false;
// 	}
//
//
// 	private IEnumerator Attack3x3ToPlayer()
// 	{
// 		MonsterAni.SetBool("CD", true);
// 		while (isAttacking)
// 		{
// 			if (MonsterAni.GetBool("CD"))
// 			{
// 				var grid = GridBuild.MyGridMap.GetGridObject(PlayerController.playerPos.x, PlayerController.playerPos.y);
// 				try
// 				{
// 					Debug.Log("攻击:" + grid.GridPos);
// 					//GameObject.Instantiate(attack, grid.GridPos, Quaternion.identity);
// 					if (isBanana)
// 					{
// 						var go = PoolSystem.GetGameObject("Damage3x3");
// 						go.transform.position = grid.GridPos;
// 					}
// 					else
// 					{
// 						var go = PoolSystem.GetGameObject("Attack3x3");
// 						go.transform.position = grid.GridPos;
// 					}
// 				}
// 				catch { Debug.Log("玩家界外"); }
//
// 				MonsterAni.SetBool("CD", false);
// 				yield return new WaitForSeconds(attackDuration);
// 			}
// 			else
// 			{
// 				MonsterAni.SetBool("CD", true);
// 				yield return new WaitForSeconds(0);
// 			}
// 		}
// 	}
//
// 	public void Dead()
// 	{
// 		StartCoroutine(nameof(FadeOut));
// 	}
//
// 	private IEnumerator FadeOut()
// 	{
// 		while (spriteRenderer.color.a > 0.05f)
// 		{
// 			spriteRenderer.color -= new Color(0, 0, 0, 0.05f);
// 			yield return new WaitForSeconds(0.01f);
// 		}
// 		Destroy(gameObject);
// 	}
// }
