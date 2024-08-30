// When the window finishes loading...
window.addEventListener('load', function() {
  // Find the first chat element in the document
  const firstChat = document.querySelector('.chat');
  
  // If a chat element is found...
  if (firstChat) {
      // Add the 'active-chat' class to the first chat element
      // This class likely signifies that the chat is currently active or visible
      firstChat.classList.add('active-chat');
  }
});

document.querySelectorAll('.left .person').forEach(person => {
  // Add a click event listener to each person element
  person.addEventListener('click', () => {
      // Log the clicked person for debugging purposes
      console.log('Person clicked:', person); 

      // Remove 'active' class from all '.left .person' elements
      document.querySelectorAll('.left .person').forEach(p => p.classList.remove('active'));
      
      // Add 'active' class to the clicked person element
      person.classList.add('active');

      // Retrieve the 'data-chat' attribute from the clicked person element
      const chatData = person.getAttribute('data-chat');
      console.log('Data-chat attribute:', chatData); 

      // Select the chat container associated with the clicked person using 'data-chat'
      const selectedChat = document.querySelector(`.chat[data-chat="${chatData}"]`);
      console.log('Selected chat:', selectedChat); 

      // Remove 'active-chat' class from all '.right .chat' elements
      document.querySelectorAll('.right .chat').forEach(chat => chat.classList.remove('active-chat'));

      // Add 'active-chat' class to the selected chat container
      selectedChat.classList.add('active-chat');

      // Retrieve the name of the person clicked
      const name = person.querySelector('.name').innerText;
      // Set the name in the chat header on the right side
      document.querySelector('.right .top .name').innerText = name;

      // Get the active chat container by its ID
      const activeChatContainer = document.getElementById(chatData);

      // If the active chat container exists, scroll it to the bottom
       // Ensure the selected chat container is scrollable and then scroll to the bottom
       let activeChatContainers = document.getElementsByClassName('chatContainer');
       // Iterate through each element and scroll it to the bottom
       for (let i = 0; i < activeChatContainers.length; i++) {
           activeChatContainers[i].scrollTop = activeChatContainers[i].scrollHeight;
       }
  });
});

  



// Listen for input events on a text input field (search bar)
document.querySelector('input[type="text"]').addEventListener('input', function(event) {
  // Get the lowercase trimmed value of the input field (search bar)
  const searchValue = event.target.value.toLowerCase().trim();

  // Find all elements with the class '.chat' (containers for chats)
  const chatContainers = document.querySelectorAll('.chat');

  // Loop through each chat container
  chatContainers.forEach(chatContainer => {
      // Find all elements with the class '.bubble' inside each chat container
      const bubbles = chatContainer.querySelectorAll('.bubble');

      // Loop through each bubble within the chat container
      bubbles.forEach(bubble => {
          // Get the text content of the bubble, trim any leading/trailing spaces
          const bubbleText = bubble.textContent.trim();
          
          // Create a regular expression with the search value (case insensitive and global)
          const regExp = new RegExp(searchValue, 'gi');

          // Replace instances of the search term in the bubble text with HTML highlighting
          const replacedHTML = bubbleText.replace(regExp, (match) => `<span class="highlight">${match}</span>`);
          
          // Update the HTML of the bubble with the replaced content (highlighted)
          bubble.innerHTML = replacedHTML;
      });
  });
});

// Listen for input events on a text input field (search bar)
document.querySelector('input[type="text"]').addEventListener('input', function(event) {
  // Get the lowercase trimmed value of the input field (search bar)
  const searchValue = event.target.value.toLowerCase().trim();

  // Find all elements with the class '.left .person' (list of people)
  const persons = document.querySelectorAll('.left .person');

  // Loop through each person element
  persons.forEach(person => {
      // Get the 'data-chat' attribute value from the person element
      const chatData = person.getAttribute('data-chat');

      // Find all chat containers associated with the current person's 'data-chat'
      const chatContainers = document.querySelectorAll('.chat[data-chat="' + chatData + '"]');

      // Initialize a flag to track if a match is found
      let foundMatch = false;

      // Loop through each chat container associated with the person
      chatContainers.forEach(chatContainer => {
          // Find all messages (bubbles) within the chat container
          const messages = chatContainer.querySelectorAll('.bubble');

          // Loop through each message bubble
          messages.forEach(message => {
              // Get the lowercase text of the message bubble
              const messageText = message.innerText.toLowerCase();

              // Check if the message text contains the search value
              if (messageText.includes(searchValue)) {
                  foundMatch = true; // Set foundMatch to true if a match is found
              }
          });
      });

      // Show or hide the person based on whether a match is found or if the search value is empty
      if (foundMatch || searchValue === '') {
          person.style.display = 'block'; // Show the person if a match is found or if the search is empty
      } else {
          person.style.display = 'none'; // Hide the person if no match is found
      }
  });
});

    
// Function to move a specific chat to the top of the list
function moveToTop(chatData) {
  // Find the chat element in the list of people (assuming it's represented as an <li>)
  const chatElement = document.querySelector(`li[data-chat="${chatData}"]`);
  
  // Find the list of people where the chat element will be moved
  const peopleList = document.querySelector('.people');
  
  // If the chat element is found...
  if (chatElement) {
      // Move the chatElement to the top of the list (prepend it)
      peopleList.prepend(chatElement);
      // This effectively moves the chat associated with 'chatData' to the top of the list
      // by repositioning the corresponding HTML element within the list of people
  }
}

  
  // establish a connection to the ChatHub
$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

    connection.start().then(function () {
        console.log('Connection established!');
    }).catch(function (err) {
        console.error('Error while starting connection: ' + err);
    });
    
      // Function to toggle the send button based on message content
      function toggleSendButton() {
          var content = $('#messageInput').val().trim();

          if (content === '') {
              $('#sendLink').prop('disabled', true);
          } else {
              $('#sendLink').prop('disabled', false);
          }
      }

      // Initial toggle of the send button
      toggleSendButton();

      // Event handler for input changes in the message input field
      $('#messageInput').on('input', function () {
          toggleSendButton();
      });

      // Event handler for keyup (Enter key) in the message input field
      $('#messageInput').on('keyup', function (event) {
          if (event.keyCode === 13) { // Check if Enter key is pressed
              $('#sendLink').click(); // Trigger click on send button
          }
      });

      // Event handler for clicking the send button
      $('#sendLink').on('click', function () {
          var content = $('#messageInput').val().trim();

          if (content !== '') {
              var activeChat = $('.chat.active-chat').attr('data-chat');

              // Invoke the server method 'SendMessageToChat' with chat and message content
              connection.invoke('SendMessageToChat', activeChat, content).catch(function (err) {
                  console.error(err.toString());
              });

              // Move the chat to the top after sending the message
              moveToTop(activeChat);
            
              // Clear the message input and disable the send button
              $('#messageInput').val('');
              $('#sendLink').prop('disabled', true);
          }
      

        });

  

  // Event listener for receiving a message from the server (other user)
  connection.on('SendMessage', function (chatname, message) {
      var audiorecive = new Audio('/audio/sendmsg.mp3');
      audiorecive.play(); // This will play the audio when new-message is added
      var chatDiv = $('.chat[data-chat="' + chatname + '"]');
      const messageDiv = $('<div class="bubble you"></div>');
      messageDiv[0].innerHTML = message;
      chatDiv.append(messageDiv);
      let activeChatContainers = document.getElementsByClassName('chatContainer');
       // Iterate through each element and scroll it to the bottom
       for (let i = 0; i < activeChatContainers.length; i++) {
           activeChatContainers[i].scrollTop = activeChatContainers[i].scrollHeight;
       }
  });

  // Event listener for receiving a message from the server (self)
  connection.on('ReceiveMessage', function (chatname, message) {
      var chatDiv = $('.chat[data-chat="' + chatname + '"]');
      const formattedMessageg = message.replace(/\\/g, ' ').replace(/"/g, ' ');

      // Replace all occurrences of '\n' with '<br>' globally in the message
      const formattedMessage = formattedMessageg.replace(/\n\n/g, '<br>');

      // Create a new <div> element to hold the message
      const messageDiv = $('<div class="bubble me"></div>');

      // Set innerHTML to the formatted message (with <br> tags)
      messageDiv[0].innerHTML = formattedMessage;
      var chatDiv1 = $('.chat.active-chat[data-chat="' + chatname + '"]');

      // Append the messageDiv to the chatDiv
      chatDiv.append(messageDiv);
      

      // Handle existing chat logic or indicate a new message
      if (chatDiv1.length > 0) {
        var audiorecive = new Audio('/audio/sendmsg.mp3');
        audiorecive.play(); // This will play the audio when new-message is added
      } else {
        var personElement = $('li.person[data-chat="' + chatname + '"]');
        if (personElement.length > 0) {
            var lastmsgElement = personElement.find('.preview');
            if (lastmsgElement.length > 0) {
                lastmsgElement.addClass('new-message');
                var audiorecivenotif = new Audio('/audio/newmsgnotif.mp3');
                    audiorecivenotif.play(); // This will play the audio when new-message is added
            }
            personElement.on('click', function() {
              lastmsgElement.removeClass('new-message');
             
          });
        }
      }
      // Move the chat to the top after receiving a message
      moveToTop(chatname);
      let activeChatContainers = document.getElementsByClassName('chatContainer');
       // Iterate through each element and scroll it to the bottom
       for (let i = 0; i < activeChatContainers.length; i++) {
           activeChatContainers[i].scrollTop = activeChatContainers[i].scrollHeight;
       }
  });
});



    
   // cordinate the chat view 
