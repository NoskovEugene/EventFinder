"use strict";

var calendars = {};

$(document).ready( function() {
    console.info(
        'Welcome to the CLNDR demo. Click around on the calendars and' +
        'the console will log different events that fire.');

    // Assuming you've got the appropriate language files,
    // clndr will respect whatever moment's language is set to.
    // moment.locale('ru');

    // Here's some magic to make sure the dates are happening this month.

    /*burger menu*/

    let burgerMenu = document.querySelector('div.burger-menu');
    let popup = document.querySelector('.pop-up');
    console.log(popup);
    console.log(burgerMenu);

    burgerMenu.onclick = function () {
        popup.classList.toggle('open-menu');
    };



    /*calendar*/

    let eventArray = $.ajax({
        type: 'GET',
        url: 'api/EventSchedule',
        dataType: 'json',
        async: false
    }).responseJSON;



    // The order of the click handlers is predictable. Direct click action
    // callbacks come first: click, nextMonth, previousMonth, nextYear,
    // previousYear, nextInterval, previousInterval, or today. Then
    // onMonthChange (if the month changed), inIntervalChange if the interval
    // has changed, and finally onYearChange (if the year changed).
   calendars.clndr1 = $('.cal-field').clndr({
        events: eventArray,
        clickEvents: {
            click: function (target) {

                $(".body-cal div.event-cal").html(function () {
                    let array = target.events;
                    let html = '';
                    if (array.length == 0) {
                        html = '<div class = "no-events"><p>No events</p></div>';
                    }
                    else {
                        for (let i = 0; array.length > i; i++) {
                            let id = array[i].id;
                            let time = array[i].time;
                            let title = array[i].title;
                            html += (('<div class = "event-item"><b>' + time + '</b><b>' + title + '</b><a href = "/Events/' + id +'"><span class="fas fa-search"></span></a></div>') +
                                ('<div class = "about-event"><p><b>Category: </b> ' + array[i].category + '</p>' + 
                                '<p><b>Creator: </b>' + array[i].owner + '</p>' + 
                                '<p><b>Place: </b> ' + array[i].place + '</p>' + 
                                '<p><b>Description: </b> ' + array[i].description + '</p></div>'));
                        }
                    }

                    return html;
                });
                
            },
            today: function () {

            },
            nextMonth: function () {
                console.log('Cal-1 next month');
            },
            previousMonth: function () {
                console.log('Cal-1 previous month');
            },
            onMonthChange: function () {
                console.log('Cal-1 month changed');
            },
            nextYear: function () {
                console.log('Cal-1 next year');
            },
            previousYear: function () {
                console.log('Cal-1 previous year');
            },
            onYearChange: function () {
                console.log('Cal-1 year changed');
            },
            nextInterval: function () {
                console.log('Cal-1 next interval');
            },
            previousInterval: function () {
                console.log('Cal-1 previous interval');
            },
            onIntervalChange: function () {
                console.log('Cal-1 interval changed');
            }
        },
        multiDayEvents: {
            singleDay: 'date',
            endDate: 'endDate',
            startDate: 'startDate'
        },
        showAdjacentMonths: true,
        adjacentDaysChangeMonth: false
    });
    // Bind all clndrs to the left and right arrow keys
    $(document).keydown( function(e) {
        // Left arrow
        if (e.keyCode == 37) {
            calendars.clndr1.back();
        }

        // Right arrow
        if (e.keyCode == 39) {
            calendars.clndr1.forward();
        }
    });
});