using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private Transform camTransform;
	private float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	[SerializeField] private float shakeAmount = 0.7f;
	[SerializeField] private float decreaseFactor = 1.0f;

	Vector3 originalPos;

	private float rotationZ;
	private float rotationX;
	private float rotationY;
	public float scaleCamera;
	public Camera cameraControl;

	[SerializeField, ReadOnly] private bool shake;
	private float shakeCount;
	private float normalCount;

	[SerializeField] private float targetSize;
	[SerializeField] private float zoomSize;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = Camera.main.transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}

		if (rotationZ != 0)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotationZ * Time.deltaTime);
			if (transform.eulerAngles.z > 20f)
				rotationZ = -rotationZ;
			else if (transform.eulerAngles.z < -20f)
				rotationZ = -rotationZ; 

			cameraControl.orthographicSize += scaleCamera;
			if (cameraControl.orthographicSize >= zoomSize)
			{
				scaleCamera = -scaleCamera;

			}
			else if (cameraControl.orthographicSize <= targetSize)
			{
				scaleCamera = -scaleCamera;
			}

		}

		if(rotationX != 0 && rotationY != 0)
		{

			transform.eulerAngles = new Vector3(transform.eulerAngles.x + (-rotationX * 2) * Time.deltaTime, transform.eulerAngles.y + (-rotationY * 2) * Time.deltaTime, transform.eulerAngles.z);
			if (transform.eulerAngles.x > 10f)
			{
				
				rotationX = -rotationX;
				rotationY = -rotationY;
			}
			else if (transform.eulerAngles.x < -10f)
			{
				rotationX = -rotationX;
				rotationY = -rotationY;
			}

			cameraControl.orthographicSize += scaleCamera;
			if (cameraControl.orthographicSize >= zoomSize)
			{
				scaleCamera = -scaleCamera;

			}
			else if (cameraControl.orthographicSize <= targetSize)
			{
				scaleCamera = -scaleCamera;
			}
		}

		if (shake)
		{
			shakeCount -= Time.deltaTime;
			if(shakeCount <= 0)
			{
				shakeCount = 0.5f;
				DoShake(0.15f);
			}
		}

		if (!shake && rotationZ == 0 && rotationX == 0 && rotationY == 0)
		{
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, 30 * Time.deltaTime);
			if (cameraControl.orthographicSize > targetSize)
			{
				cameraControl.orthographicSize -= 15 * Time.deltaTime/2;
			}
			else
			{
				cameraControl.orthographicSize = targetSize;
			}
			normalCount -= Time.deltaTime;
			if(normalCount <=0)
			{
				transform.eulerAngles = new Vector3(0,0,0);
				normalCount = 1f;
			}
		}
	}

	public void DoShake(float time)
	{
		shakeDuration = time;
	}

	public void DoRotation(float rotationOffset)
	{
		rotationZ = rotationOffset;
		scaleCamera = Time.deltaTime / 2;
	}

	public void RandomRave(float rotationOffset)
	{
		int randomNumber = Random.Range(0, 4);
		if(randomNumber == 0)
		{
			rotationZ = rotationOffset;
			scaleCamera = Time.deltaTime / 2;
		}
		else if(randomNumber == 1)
		{
			rotationX = rotationOffset;
			rotationY = rotationOffset;
			scaleCamera = 0;
		}
		else if (randomNumber == 2)
		{
			rotationX = rotationOffset;
			rotationY = -rotationOffset;
			scaleCamera = Time.deltaTime / 2;
		}
		else if (randomNumber == 3)
		{
			shake = true;
		}
	}

	public void StopRotation()
	{
		rotationZ = 0;
		rotationX = 0;
		rotationY = 0;
		scaleCamera = 0f;
		shake = false;
	}
}
