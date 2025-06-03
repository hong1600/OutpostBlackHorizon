using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableTester : MonoBehaviour
{
    public string testAddress = "Robot1";

    private void Start()
    {
        StartCoroutine(CheckAddressableAddress(testAddress));
    }

    IEnumerator CheckAddressableAddress(string address)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(address);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"[�׽�Ʈ ����] {address} �ε� �Ϸ�: {handle.Result.name}");
            Addressables.Release(handle);
        }
        else
        {
            Debug.LogError($"[�׽�Ʈ ����] {address} �ε� ����: ���� {handle.Status}");
            if (handle.OperationException != null)
                Debug.LogError(handle.OperationException.Message);
        }
    }
}
