import base64, httpx
import os
import google.generativeai as genai
import time

genai.configure(api_key=os.environ.get('GOOGLE_API_KEY'))

safety_settings = [
    {
        "category": "HARM_CATEGORY_HATE_SPEECH",
        "threshold": "BLOCK_NONE",
    },
    {
        "category": "HARM_CATEGORY_SEXUALLY_EXPLICIT",
        "threshold": "BLOCK_NONE",
    },
    {
        "category": "HARM_CATEGORY_DANGEROUS_CONTENT",
        "threshold": "BLOCK_NONE",
    },
    {
        "category": "HARM_CATEGORY_HARASSMENT",
        "threshold": "BLOCK_NONE",
    }
]

model = genai.GenerativeModel('gemini-1.5-flash', safety_settings=safety_settings)

def bard(text, model=model): 
    for attempt in range(3):
        try:
            response = model.generate_content(text)
            time.sleep(1)
            if not response or not hasattr(response, 'text'):
                raise ValueError("Invalid operation: The `response.text` quick accessor requires the response to contain a valid `Part`, but none were returned. Please check the `candidate.safety_ratings` to determine if the response was blocked.")
            return response.text
        except ValueError as e:
            print(f"Attempt {attempt + 1} failed: {e}")
            if attempt == 2:
                print("Continuing after 3 failed attempts.")
                return None