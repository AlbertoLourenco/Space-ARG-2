using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public ShotType type;

    [SerializeField]
    private float _speed = 10.0f;

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Update () {

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 5.43f) {
            
            Destroy(this.gameObject);
        }
	}

    //----------------------------------------------------------------------------------
    //  Collision detect
    //----------------------------------------------------------------------------------

	private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Enemy") {

            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Damage(25);

            switch (enemy.type) {
                case EnemyType.Big:
                    GameManager.IncreaseScore(2);
                    break;
                case EnemyType.Normal:
                    GameManager.IncreaseScore(1);
                    break;
            }

            Destroy(this.gameObject, 0);
        }
	}
}
