using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Rewind : MonoBehaviour
{
    public float maxRewindDuration = 20f;
    public float rewindSpeed = 250f;
    private bool isRewinding = false;
    public GameObject overlayPopup;

    private List<TimeSnapshot> timeSnapshots = new List<TimeSnapshot>();

    private struct TimeSnapshot
    {
        public Vector3 position;
        public Quaternion rotation;

        public TimeSnapshot(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (overlayPopup != null)
                overlayPopup.SetActive(true);
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            if (overlayPopup != null)
                overlayPopup.SetActive(false);
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (!isRewinding)
        {
            RecordSnapshot();
        }

        if (isRewinding)
        {
            RewindTime();
        }
    }

    private void RecordSnapshot()
    {
        timeSnapshots.Insert(0, new TimeSnapshot(transform.position, transform.rotation));

        if (timeSnapshots.Count > Mathf.Round(maxRewindDuration / Time.fixedDeltaTime))
        {
            timeSnapshots.RemoveAt(timeSnapshots.Count - 1);
        }
    }

    private void RewindTime()
    {
        if (timeSnapshots.Count > 0)
        {
            TimeSnapshot snapshot = timeSnapshots[0];
            transform.position = snapshot.position;
            transform.rotation = snapshot.rotation;

            timeSnapshots.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;

    }

    public void StopRewind()
    {
        isRewinding = false;
    }
}
