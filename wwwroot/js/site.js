'use strict'
$(document).ready(function() {
    let calField = document.querySelector('.cal-field');
    let eventCal = document.querySelector('.event-cal');
    let calIcon = document.querySelector('.cal-icon');
    
    calIcon.onclick = function() {
        eventCal.classList.toggle('open');
        if(!eventCal.classList.contains('open')){
            eventCal.classList.add('close');
        }
        else {
            eventCal.classList.remove('close');
        }
    };
    
    
    /*burger menu*/
    
    let burgerMenu = document.querySelector('div.burger-menu');
    let popup = document.querySelector('.pop-up');
    console.log(popup);
    console.log(burgerMenu);
    
    burgerMenu.onclick = function() {
        popup.classList.toggle('open-menu');
    };
    
     /*show event*/
    
    //let eventDesc = document.querySelector('.event-block');
    
    /*$('.desc-event').hover(function() {
        let parent = $(this).closest('.event-block');
        console.log(parent.classList);
        let img = $(this).prev();
        console.log(img);
        let span = $(this).children('span');
        span.text(img.attr('alt'));
    });*/
});