using System;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using UnityEngine;

namespace DefaultNamespace.SampleFactory
{
    public class BearFactory : MonoBehaviour
    {
        private IFactory _factory;
        private void Awake()
        {
            _factory = new Factory(
                new AddressableAssetsProvider(AssetPath.GetRemoteCatalogPath(), OnInitializeComplete));
        }

        private async void OnInitializeComplete()
        {
            GameObject result = await _factory.CreateObject("Bear0");
            if (result.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            result.transform.position = new Vector3(0, 1.5f, 1);
            GameObject result1 = await _factory.CreateObject("Bear1");
            if (result1.TryGetComponent(out Rigidbody rb1))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            result.transform.position = new Vector3(0, 1.5f, 1);
            GameObject result2 = await _factory.CreateObject("Bear2");
            if (result2.TryGetComponent(out Rigidbody rb2))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            result.transform.position = new Vector3(0, 1.5f, 1);
            GameObject result3 = await _factory.CreateObject("Bear3");
            if (result3.TryGetComponent(out Rigidbody rb3))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            result.transform.position = new Vector3(0, 1.5f, 1);
            GameObject result4 = await _factory.CreateObject("Bear4");
            if (result4.TryGetComponent(out Rigidbody rb4))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            result.transform.position = new Vector3(0, 1.5f, 1);
            GameObject result5 = await _factory.CreateObject("Bear5");
            if (result5.TryGetComponent(out Rigidbody rb5))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            result.transform.position = new Vector3(0, 1.5f, 1);
        }
    }
}