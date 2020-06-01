using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text movesLeftText;
    [SerializeField] Transform holePosition;
    [SerializeField] Transform ball;
    List<Course> courses;
    public Course ActiveCourse { get; private set; }
    int movesMade;
    private void Awake()
    {
        courses = new List<Course>();
        foreach (var course in GetComponentsInChildren<Course>())
        {
            courses.Add(course);
        }
        ActiveCourse = courses[Random.Range(0, courses.Count)];
        ResetCourse();
    }

    Course GetRandomCourse()
    {
        if (courses.Count <=1)
        {
            return null;
        }
        Course course=ActiveCourse;
        while (ActiveCourse == course)
        {
            course=courses[Random.Range(0, courses.Count)];
        }
        return course;
    }
    [ContextMenu("Next")]
    public void ActivateRandomCourse()
    {
        ActiveCourse=GetRandomCourse();
        ResetCourse();
    }
    [ContextMenu("reset")]
    public void ResetCourse()
    {
        holePosition.position = ActiveCourse.HolePosition.position;
        ball.position = ActiveCourse.transform.position;
        movesMade = 0;
        UpdateUI();
    }

    public bool ShotMade()
    {
        movesMade++;
        UpdateUI();
        if (ActiveCourse.MaxMovesAllowed - movesMade <= 0)
        {
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        if(ActiveCourse)
            movesLeftText.text = "Moves Left:" + (ActiveCourse.MaxMovesAllowed - movesMade);
    }
}
