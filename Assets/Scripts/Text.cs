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
            Debug.Log($"[테스트 성공] {address} 로드 완료: {handle.Result.name}");
            Addressables.Release(handle);
        }
        else
        {
            Debug.LogError($"[테스트 실패] {address} 로드 실패: 상태 {handle.Status}");
            if (handle.OperationException != null)
                Debug.LogError(handle.OperationException.Message);
        }
    }
}
