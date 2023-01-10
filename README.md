<div align="center">
 
<h1 align="center">Tweet Mood</h1>

[![](https://img.shields.io/badge/Backend_Made_with-.NET-red?style=for-the-badge&logo=.net)](#)
[![](https://img.shields.io/badge/Chrome_Extension_Made_with-Javascript-orange?style=for-the-badge&logo=javascript)](#)
[![](https://img.shields.io/badge/IDE-Visual_Studio_Code-purple?style=for-the-badge&logo=visual-studio-code)](#)
[![](https://img.shields.io/badge/Hosted_On-Azure-blue.svg?style=for-the-badge&logo=microsoft-azure&logoColor=white)](#)
</div>

<p align="center">
Tweet Mood is a Chrome Extension designed to analyse the sentiment of the tweets and display an approriate emoji amongst 😊, 😐, ☹️ beside the date of the tweet to show the mood.
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#technologies">Tech</a></li>
       <li><a href="#cloud-services">Cloud Services</a></li>
       <li><a href="#features">Features</a></li>
       <li><a href="#features">Demo</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

### Technologies 

* [.NET](#)
* [Javascript](#)
* [YML](#)

### Cloud Services

* Azure(App Service, Language Service)
* GitHub Actions (Continuous integration & deployment)

### Features

#### General

- [x] Easy to load chrome extension
- [x] Display mood of the tweet using emoji

#### APIs

- [x] Language Detection: POST https://tweetmood.azurewebsites.net/api/language-detection
  ![api](./MediaContent/Language_Detection_Api.png)
- [x] Sentiment Detection: POST https://tweetmood.azurewebsites.net/api/sentiment-score
  ![api](./MediaContent/Sentiment_Score_Api.png)

<!-- DEMO -->
#### Demo
 ![api](./MediaContent/Demo.gif)


<!-- CONTACT -->
## Contact

Shreyas  - shreyasm@usc.edu
