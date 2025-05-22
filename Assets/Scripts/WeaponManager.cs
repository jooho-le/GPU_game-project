using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weaponPrefab; // 무기 프리팹 (Weapon 스크립트 포함)
    public Transform weaponParent;  // 무기를 배치할 부모 (보통 플레이어 오브젝트)

    [SerializeField] private int weaponcount = 1;

    private List<GameObject> weapons = new List<GameObject>();
    private Weapon weapon;

    void Start()
    {
        UpdateWeaponLayout();
        weapon = FindObjectOfType<Weapon>();
    }

    public void IncreaseWeapon()
    {
        weaponcount++;
        UpdateWeaponLayout();
        SetAllWeaponAttackPower(weapon.attackPower);
    }

    void UpdateWeaponLayout()
    {
        // 기존 무기 개수보다 더 많이 필요하면 새로 생성
        while (weapons.Count < weaponcount)
        {
            GameObject newWeapon = Instantiate(weaponPrefab, weaponParent);
            newWeapon.transform.localScale = new Vector3(1.1244f, 1.0758f, 1f);
            weapons.Add(newWeapon);
        }

        // 불필요한 무기는 비활성화
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i < weaponcount);
        }

        // 원형으로 재배치
        float radius = 0.5f; // 중심으로부터의 거리
        for (int i = 0; i < weaponcount; i++)
        {
            float angle = i * (360f / weaponcount);
            Vector3 pos = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0f
            ) * radius;

            weapons[i].transform.localPosition = pos;
        }
    }

    public void SetAllWeaponAttackPower(float newPower)
    {
        foreach (GameObject weaponObj in weapons)
        {
            Weapon weapon = weaponObj.GetComponent<Weapon>();
            if (weapon != null)
            {
                weapon.attackPower = newPower;
            }
        }
    }
}
