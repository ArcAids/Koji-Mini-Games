using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text movesLeftText;
    [SerializeField] TMPro.TMP_Text parText;
    [SerializeField] TMPro.TMP_Text resultText;
    [SerializeField] TMPro.TMP_Text finalResultText;
    [SerializeField] TMPro.TMP_Text goalsText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Transform holePosition;
    [SerializeField] GameObject winScreen;
    [SerializeField] Transform ball;
    List<Course> courses;
    public Course ActiveCourse { get; private set; }
    int currentIndex;
    int movesMade;
    int totalShots;
    int totalPar;
    private void Awake()
    {
        courses = new List<Course>();
        foreach (var course in GetComponentsInChildren<Course>())
        {
            courses.Add(course);
            totalPar += course.MaxMovesAllowed;
        }
        currentIndex = -1;
        ActivateNextCourse();
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

    int GetRandomCourseIndex()
    {
        if (courses.Count <= 1)
        {
            Debug.Log("no courses to randomize!");
            return 0;
        }
        int index = currentIndex;
        while (currentIndex == index)
        {
            index=Random.Range(0, courses.Count);
        }
        return index;
    }

    [ContextMenu("Next")]
    public void ActivateRandomCourse()
    {
        ActiveCourse = courses[GetRandomCourseIndex()];
        ResetCourse();
    }
    public void ActivateNextCourse()
    {
        if(currentIndex == (courses.Count - 1))
        {
            OnFinish();
            return;
        }
        else
        {
            currentIndex++;
        }
        ActiveCourse = courses[currentIndex];
        ResetCourse();
    }
    [ContextMenu("reset")]
    public void ResetCourse()
    {
        parText.text = "Par: " + ActiveCourse.MaxMovesAllowed;
        goalsText.text = "Hole #" + (currentIndex+1);
        winScreen.SetActive(false);
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
            movesLeftText.text = "Strokes: " + (movesMade);
    }

    public void OnHole()
    {
        totalShots += movesMade;

        string result="Par.";
        int par = (movesMade - ActiveCourse.MaxMovesAllowed);

        if (movesMade == 1)
            result = "Ace!!";
        else if (par == -1)
            result = "Birdie!! -1";
        else if (par == 1)
            result = "Bogey! +1";
        else if (par < 0)
            result = par + " under par!!";
        else if (par > 0)
            result = par + " over par.";

        resultText.text = result;
        winScreen.SetActive(true);
    }

    public void OnFinish()
    {
        parText.text = "Total Par: " + totalPar;
        movesLeftText.text = "Total strokes: " + (totalShots);
        goalsText.text = "Holes: " + (currentIndex + 1);
        finalResultText.text = "Final score: "+ (totalShots-totalPar);
        gameOverScreen.SetActive(true);
        winScreen.SetActive(false);

    }
}
