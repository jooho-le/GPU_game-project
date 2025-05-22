using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weaponPrefab; // ���� ������ (Weapon ��ũ��Ʈ ����)
    public Transform weaponParent;  // ���⸦ ��ġ�� �θ� (���� �÷��̾� ������Ʈ)

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
        // ���� ���� �������� �� ���� �ʿ��ϸ� ���� ����
        while (weapons.Count < weaponcount)
        {
            GameObject newWeapon = Instantiate(weaponPrefab, weaponParent);
            newWeapon.transform.localScale = new Vector3(1.1244f, 1.0758f, 1f);
            weapons.Add(newWeapon);
        }

        // ���ʿ��� ����� ��Ȱ��ȭ
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i < weaponcount);
        }

        // �������� ���ġ
        float radius = 0.5f; // �߽����κ����� �Ÿ�
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
