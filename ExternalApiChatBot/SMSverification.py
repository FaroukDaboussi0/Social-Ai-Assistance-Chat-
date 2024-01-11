from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI()

# Replace 'your_msg91_api_key' with your actual MSG91 API key
MSG91_API_KEY = '413223AO4rVhGdwVtZ65988d20P1'

class PhoneNumber(BaseModel):
    number: str

class VerificationData(BaseModel):
    phoneNumber: str
    code: str

def send_verification_code(phone_number):
    url = f"https://api.msg91.com/api/v5/otp?authkey={MSG91_API_KEY}"
    payload = {
        "message": "Your verification code is {{code}}.",
        "mobile": phone_number,
        "sender": "SENDER_ID"  # Replace SENDER_ID with your sender ID
        # Additional parameters for MSG91 can be added here as required
    }
    response = requests.post(url, json=payload)
    return response.json()

def verify_code(phone_number, user_entered_code):
    url = f"https://api.msg91.com/api/v5/otp/verify?authkey={MSG91_API_KEY}"
    payload = {
        "otp": user_entered_code,
        "mobile": phone_number
    }
    response = requests.post(url, json=payload)
    return response.json()['type']  # Adjust this based on MSG91 response

@app.post("/api/send")
async def send_code(phone_number: PhoneNumber):
    response = send_verification_code(phone_number.number)
    return {"status": response['type']}  # Adjust based on MSG91 response

@app.post("/api/verify")
async def verify_code(verification_data: VerificationData):
    response = verify_code(verification_data.phoneNumber, verification_data.code)
    return {"verification_status": response}
