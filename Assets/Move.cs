using System.Collections;
using System.Collections.Generic;
using Assets.HealthBar;
using UnityEngine;

public class Move : MonoBehaviour
{

    private Transform _tarTransform;
    private Controller _controller;

    void Start()
    {
        _tarTransform = transform;
        _controller = GameObject.Find("Controller").GetComponent<Controller>();
    }

	// Update is called once per frame
	void Update ()
	{
	    Vector3 pos = _tarTransform.position;
	    if (Input.GetKey(KeyCode.W))
	    {
	        pos.z += 0.01f;
	    }
	    if (Input.GetKey(KeyCode.S))
	    {
	        pos.z -= 0.01f;
	    }
	    if (Input.GetKey(KeyCode.A))
	    {
	        pos.x -= 0.01f;
	    }
	    if (Input.GetKey(KeyCode.D))
	    {
	        pos.x += 0.01f;
	    }
	    _tarTransform.position = pos;
        Vector3 ps = Camera.main.WorldToViewportPoint(transform.position);
        _controller.Rect.anchoredPosition = new Vector2(
            _controller.CanvasRectTransform.sizeDelta.x * ps.x - _controller.CanvasRectTransform.sizeDelta.x / 2,
            _controller.CanvasRectTransform.sizeDelta.y * ps.y - _controller.CanvasRectTransform.sizeDelta.y / 2);
        this.Log(ps + " ");

    }
}
